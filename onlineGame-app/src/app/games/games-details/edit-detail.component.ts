import { Component } from '@angular/core';
import { GameService } from '../../core/game.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { forkJoin } from 'rxjs';
import { BaseGameFormComponent } from './game-form.component';
import { ConfirmationDialogService } from '../confirmation-dialog/confirmation-dialog.service';
import { finalize } from 'rxjs/operators';
import { IGame } from 'src/app/models/IGame';
import { IPlatformType } from 'src/app/models/IPlatformType';
import { IGenre } from 'src/app/models/IGenre';

@Component({
  selector: 'app-edit-games-selector',
  templateUrl: './edit-detail.component.html',
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
export class GameEditComponent extends BaseGameFormComponent {
  id: string;
  game: IGame;

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
    this.id = this.route.snapshot.paramMap.get('id');
    const observables = [this.gameService.getPlatformTypes(),  this.gameService.getGenres(), this.gameService.getGame(this.id)];
    forkJoin(observables)
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe(([platformTypes , genres, game]) => {
        this.dropdownList = (platformTypes as IPlatformType[]).map((x: IPlatformType) => ({
          item_id: x.id,
          item_text: x.type
        }));
        this.dropdownListGenres = (genres as IGenre[])
          .filter(y => y.parentGenre === null)
          .map(x => ({
            item_id: x.id,
            item_text: x.name
          }));
        this.game = game as IGame;
        this.selectedItems = this.game.platformTypes.map(x => ({
          item_id: x.id,
          item_text: x.type
        }));
        this.selectedItemsGenre = this.game.genres.map(x => ({
          item_id: x.id,
          item_text: x.name
        }));
        this.name.setValue(this.game.name);
        this.description.setValue(this.game.description);
      });
  }

  validateForm() {
    return (
      this.gameForm.valid &&
      (this.description.dirty ||
        this.name.dirty ||
        this.onSelectAllIsTriggered ||
        this.onItemSelectGenreIsTriggered)
    );
  }

  saveGame(formValues: { name: string; description: string; }) {
    const session = {
      name: formValues.name,
      description: formValues.description,
      publisherId: undefined,
      genresId: this.selectedItemsGenre.map(x => x.item_id),
      platformTypesId: this.selectedItems.map(x => x.item_id)
    };

    if (this.gameForm.valid) {
      this.markAsPristine();

      this.gameService.putGame(this.id, JSON.stringify(session)).subscribe();
      this.router.navigate(['/games']);
    }
  }
}
