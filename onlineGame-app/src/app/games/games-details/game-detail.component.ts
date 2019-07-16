import { Component } from '@angular/core';
import { GameService } from "../game.service";
import { Router } from '@angular/router';
import { BaseGameFormComponent } from './game-form.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute } from "@angular/router";
import { forkJoin } from 'rxjs';
import { ConfirmationDialogService } from '../confirmation-dialog/confirmation-dialog.service';

@Component({
  templateUrl: './game-detail.component.html',
  styles:[`
    em {float:right; color:#E05C65; padding-left:10px;}
    .error input, .error select, .error textarea {background-color:#E3C3C5;}
    .error ::-webkit-input-placeholder { color: #999; }
    .error :-moz-placeholder { color: #999; }
    .error ::-moz-placeholder {color: #999; }
    .error :ms-input-placeholder { color: #999; }
    .btn-primary {
      margin-right: 5px;
    }
  `]

})

export class GameDetailComponent extends BaseGameFormComponent {

  constructor(protected spinner: NgxSpinnerService,
    protected route: ActivatedRoute,
    protected gameService: GameService,
    protected router: Router, protected confirmationDialogService: ConfirmationDialogService) {
      super(spinner, route, gameService, router,confirmationDialogService);
    }
 
  fillExistInfo(): void {

    forkJoin(this.gameService.getPlatformTypes(), this.gameService.getGenres())
      .subscribe(
        pt => {

          this.dropdownList = pt[0].map((x) => ({
            item_id: x.id,
            item_text: x.type

          }));
          this.dropdownListGenres = pt[1].filter(y => y.parentGenre === null).map((x) => ({
            item_id: x.id,
            item_text: x.name
          }));
          this.spinner.hide();
        }
    );
  }

  saveGame(formValues) {
    const session = {
      name: formValues.name,
      description: formValues.description,
      publisherId: undefined,
      genresId: this.selectedItemsGenre.map(x => (x.item_id)),
      platformTypesId: this.selectedItems.map(x => (x.item_id))
    };
    debugger;
    if (this.gameForm.valid) {
      this.markAsPristine();
      this.gameService.postGame(JSON.stringify(session)).subscribe();
      this.router.navigate(['/games']);
    }

  }
}
