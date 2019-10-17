import { Component } from '@angular/core';
import { GameService } from '../../core/game.service';
import { Router } from '@angular/router';
import { BaseGameFormComponent } from './game-form.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { ConfirmationDialogService } from '../confirmation-dialog/confirmation-dialog.service';
import { finalize } from 'rxjs/operators';

@Component({
  templateUrl: './game-detail.component.html',
  styles: [
    `
      em {
        float: right;
        color: #e05c65;
        padding-left: 10px;
      }
      .error input,
      .error select,
      .error textarea {
        background-color: #e3c3c5;
      }
      .error ::-webkit-input-placeholder {
        color: #999;
      }
      .error :-moz-placeholder {
        color: #999;
      }
      .error ::-moz-placeholder {
        color: #999;
      }
      .error :ms-input-placeholder {
        color: #999;
      }
      .btn-primary {
        margin-right: 5px;
      }
    `
  ]
})
export class GameDetailComponent extends BaseGameFormComponent {
  constructor(
    protected spinner: NgxSpinnerService,
    protected route: ActivatedRoute,
    protected gameService: GameService,
    protected router: Router,
    protected confirmationDialogService: ConfirmationDialogService
  ) {
    super(spinner, route, gameService, router, confirmationDialogService);
  }

  fillExistInfo(): void {
    forkJoin([this.gameService.getPlatformTypes(), this.gameService.getGenres()])
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe(pt => {
        this.dropdownList = pt[0].map(x => ({
          item_id: x.id,
          item_text: x.type
        }));
        this.dropdownListGenres = pt[1]
          .filter(y => y.parentGenre === null)
          .map(x => ({
            item_id: x.id,
            item_text: x.name
          }));
      });
  }

  saveGame(formValues: { name: any; description: any; }) {
    const session = {
      name: formValues.name,
      description: formValues.description,
      publisherId: undefined,
      genresId: this.selectedItemsGenre.map(x => x.item_id),
      platformTypesId: this.selectedItems.map(x => x.item_id)
    };
    if (this.gameForm.valid) {
      this.markAsPristine();
      this.gameService.postGame(JSON.stringify(session)).subscribe();
      this.router.navigate(['/games']);
    }
  }
}
