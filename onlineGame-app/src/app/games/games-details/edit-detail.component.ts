import { Component} from '@angular/core'
import { GameService } from "../game.service";
import { Router } from '@angular/router';
import { ActivatedRoute } from "@angular/router";
import { IGame } from "../gameModel";
import { NgxSpinnerService } from 'ngx-spinner';
import { forkJoin } from 'rxjs';
import { BaseGameFormComponent } from './game-form.component';
import { ConfirmationDialogService } from '../confirmation-dialog/confirmation-dialog.service';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'edit-games',
  templateUrl: './edit-detail.component.html',
  styles: [
    `
    em {float:right; color:#E05C65; padding-left:10px;}
    .error input, .error select, .error textarea {background-color:#E3C3C5;}
    .error ::-webkit-input-placeholder { color: #999; }
    .error :-moz-placeholder { color: #999; }
    .error ::-moz-placeholder {color: #999; }
    .error :ms-input-placeholder { color: #999; }
    .btn-primary {
      margin-right: 5px;
    }
  `
  ]

})
export class GameEditComponent extends BaseGameFormComponent {
  id: string;
  game: IGame;

  constructor(protected spinner: NgxSpinnerService,
    protected route: ActivatedRoute,
    protected gameService: GameService,
    protected router: Router, protected confirmationDialogService: ConfirmationDialogService) {
    super(spinner, route, gameService, router,confirmationDialogService);
  }

  fillExistInfo(): void {
    this.id = this.route.snapshot.paramMap.get("id");
    forkJoin(this.gameService.getPlatformTypes(),
        this.gameService.getGenres(),
        this.gameService.getGame(this.id))
      .pipe(finalize(() => {
        this.spinner.hide();
      }))
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
          this.game = pt[2];
          this.selectedItems = this.game.platformTypes.map((x) => ({
            item_id: x.id,
            item_text: x.type
          }));
          this.selectedItemsGenre = this.game.genres.map((x) => ({
            item_id: x.id,
            item_text: x.name
          }));
          this.name.setValue(this.game.name);
          this.description.setValue(this.game.description);
        }
      );
  }

  validateForm() {
    return this.gameForm.valid &&
      (this.description.dirty || this.name.dirty || this.onSelectAllIsTriggered || this.onItemSelectGenreIsTriggered);
  }

  saveGame(formValues) {
    const session = {
      name: formValues.name,
      description: formValues.description,
      publisherId: undefined,
      genresId: this.selectedItemsGenre.map(x => (x.item_id)),
      platformTypesId: this.selectedItems.map(x => (x.item_id))
    };

    if (this.gameForm.valid) {
      this.markAsPristine();

      this.gameService.putGame(this.id, JSON.stringify(session)).subscribe();
      this.router.navigate(['/games']);
    }
  }
}
