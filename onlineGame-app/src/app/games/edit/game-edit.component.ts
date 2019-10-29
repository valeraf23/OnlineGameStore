import { Component, OnInit, Injector, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IGame } from 'src/app/models/IGame';
import { IAddGame } from 'src/app/models/IAddGame';
import { ConfirmationDialogService } from '../confirmation-dialog/confirmation-dialog.service';
import { GameService } from 'src/app/core/game.service';
import { GameResolved } from 'src/app/models/game-resolved';
import { IGenre } from 'src/app/models/IGenre';
import { IPlatformType } from 'src/app/models/IPlatformType';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs/operators';
import { Guid } from 'guid-typescript';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CanComponentDeactivate } from 'src/app/core/can-deactivate-component.interface';
import { Toastr, TOASTR_TOKEN } from 'src/app/shared/toast.services';

@Component({
  selector: 'app-edit-games-selector',
  templateUrl: './edit-detail.component.html',
  styleUrls: ['./edit-detail.component.css']
})
export class GameEditComponent implements OnInit, CanComponentDeactivate {
  pageTitle = 'Game Edit';
  errorMessage: string;

  gameForm: FormGroup;
  name: FormControl;
  platformTypes: FormControl;
  genres: FormControl;
  description: FormControl;
  dropdownList = [];
  dropdownListGenres = [];
  selectedItems = [];
  selectedItemsGenre = [];
  onSelectAllIsTriggered = false;
  onItemSelectGenreIsTriggered = false;

  dropdownSettings = {
    singleSelection: false,
    idField: 'item_id',
    textField: 'item_text',
    selectAllText: 'Select All',
    unSelectAllText: 'UnSelect All',
    itemsShowLimit: 3,
    allowSearchFilter: true
  };

  private dataIsValid: { [key: string]: boolean } = {};

  private currentProduct: IAddGame = GameEditComponent.getDefault();

  private originalProduct: IAddGame;

  static getDefault(): IAddGame {
    return {
      id: Guid.createEmpty(),
      name: undefined,
      description: undefined,
      genresId: [],
      platformTypesId: []
    };
  }
  get game(): IAddGame {
    return this.currentProduct;
  }
  set game(value: IAddGame) {
    this.currentProduct = value;
    this.originalProduct = { ...value };
  }

  constructor(
    private spinner: NgxSpinnerService,
    private confirmationDialogService: ConfirmationDialogService,
    private gameService: GameService,
    private route: ActivatedRoute,
    private router: Router,
    @Inject(TOASTR_TOKEN) private toaster: Toastr
  ) {}

  get isDirty(): boolean {
    return (
      JSON.stringify(this.originalProduct) !==
      JSON.stringify(this.currentProduct)
    );
  }

  isCanSave(): boolean {
    this.getFormValues();
    return this.isDirty && this.isValid();
  }

  getFormValues(): void {
    const session: IAddGame = {
      id: this.game.id,
      name: this.name.value,
      description: this.description.value,
      genresId: this.selectedItemsGenre.map(x => x.item_id),
      platformTypesId: this.selectedItems.map(x => x.item_id)
    };
    this.currentProduct = session;
  }
  async openConfirmationDialog(): Promise<boolean> {
    try {
      const confirmed = await this.confirmationDialogService.confirm(
        'Please confirm..',
        'Leaving this page will lose your changes. Are you sure want to leave this page ?'
      );
      if (confirmed) {
      }
      return confirmed;
    } catch (e) {
      return false;
    }
  }
  ngOnInit(): void {
    this.initForm();
    this.route.data
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe(data => {
        const resolvedData: GameResolved = data.info;
        this.errorMessage = resolvedData.error;
        const game = resolvedData.resolvedData.game;
        const genres = resolvedData.resolvedData.genres;
        const platformTypes = resolvedData.resolvedData.platformTypes;
        this.onGameRetrieved(game, genres, platformTypes);
      });
  }

  gameToAddGameModel(game: IGame): IAddGame {
    const addGame: IAddGame = {
      id: game.id,
      name: game.name,
      description: game.description,
      genresId: game.genres.map(x => x.id),
      platformTypesId: game.platformTypes.map(x => x.id)
    };
    return addGame;
  }

  async canDeactivate(): Promise<boolean> {
    if (this.isDirty) {
      return await this.openConfirmationDialog();
    }
    return true;
  }

  setGameInfo(
    platformTypes: IPlatformType[],
    genres: IGenre[],
    game: IGame
  ): void {
    this.dropdownList = platformTypes.map((x: IPlatformType) => ({
      item_id: x.id,
      item_text: x.type
    }));
    this.dropdownListGenres = genres
      .filter(y => y.parentGenre === null)
      .map(x => ({
        item_id: x.id,
        item_text: x.name
      }));
    if (game) {
      this.game = this.gameToAddGameModel(game);
      this.selectedItems = platformTypes
        .filter(p => game.platformTypes.some(gp => gp.id === p.id))
        .map(x => ({
          item_id: x.id,
          item_text: x.type
        }));
      this.selectedItemsGenre = genres
        .filter(p => game.genres.some(gp => gp.id === p.id))
        .map(x => ({
          item_id: x.id,
          item_text: x.name
        }));
      this.name.setValue(this.game.name);
      this.description.setValue(this.game.description);
    }
  }

  onGameRetrieved(
    game: IGame,
    genres: IGenre[],
    platformTypes: IPlatformType[]
  ): void {
    this.setGameInfo(platformTypes, genres, game);
    if (this.game.id.toString() === Guid.createEmpty().toString()) {
      this.pageTitle = 'Add Game';
    } else {
      this.pageTitle = `Edit Game: ${this.game.name}`;
    }
  }

  async deleteProduct(): Promise<void> {
    if (!this.game.id.toString()) {
      this.onSaveComplete(`${this.game.name} was deleted`);
    } else {
      try {
        const confirmed = await this.confirmationDialogService.confirm(
          'Please confirm..',
          `Really delete the game: ${this.game.name}?`
        );
        if (confirmed) {
          this.onSaveComplete(`${this.game.name} was deleted`);
        }
      } catch (e) {
        this.errorMessage = e;
      }
    }
  }

  isValid(path?: string): boolean {
    this.validate();
    if (path) {
      return this.dataIsValid[path];
    }
    return (
      this.dataIsValid &&
      Object.keys(this.dataIsValid).every(d => this.dataIsValid[d] === true)
    );
  }

  reset(): void {
    this.dataIsValid = null;
    this.currentProduct = null;
    this.originalProduct = null;
  }

  save(): void {
    if (this.isValid()) {
      if (this.game.id.toString() === Guid.EMPTY) {
        this.gameService.postGame(JSON.stringify(this.game)).subscribe({
          next: () =>
            this.onSaveComplete(`The new ${this.game.name} was saved`),
          error: err => (this.errorMessage = err)
        });
      } else {
        this.gameService
          .putGame(this.game.id.toString(), JSON.stringify(this.game))
          .subscribe({
            next: () =>
              this.onSaveComplete(`The updated ${this.game.name} was saved`),
            error: err => (this.errorMessage = err)
          });
      }
    } else {
      this.errorMessage = 'Please correct the validation errors.';
    }
  }

  onSaveComplete(message?: string): void {
    if (message) {
      this.toaster.success(message);
    }
    this.reset();
    this.back();
  }

  validate(): void {
    this.dataIsValid = {};
    if (
      this.game.name &&
      this.name.value.length >= 5 &&
      this.game.description &&
      this.game.description.length >= 3 &&
      this.game.genresId.length > 0 &&
      this.game.platformTypesId.length > 0
    ) {
      this.dataIsValid.info = true;
    } else {
      this.dataIsValid.info = false;
    }
  }

  initForm(): void {
    this.name = new FormControl(null, Validators.required);
    this.platformTypes = new FormControl(null, Validators.required);
    this.genres = new FormControl(null, Validators.required);
    this.description = new FormControl(null, [
      Validators.required,
      Validators.maxLength(400)
    ]);

    this.gameForm = new FormGroup({
      name: this.name,
      platformTypes: this.platformTypes,
      genres: this.genres,
      description: this.description
    });
  }

  cancel(): void {
    this.back();
  }

  back(): void {
    this.router.navigate(['/games'], { queryParamsHandling: 'preserve' });
  }
}
