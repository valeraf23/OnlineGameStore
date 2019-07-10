import { OnInit, AfterViewInit } from '@angular/core'
import { FormControl, FormGroup, Validators } from '@angular/forms'
import { GameService } from "../game.service";
import { Router } from '@angular/router';
import { ActivatedRoute } from "@angular/router";
import { NgxSpinnerService } from 'ngx-spinner';

export abstract class BaseGameFormComponent implements OnInit, AfterViewInit {

  abstract fillExistInfo(): void;

  abstract saveGame(formValues): void;

  gameForm: FormGroup;
  name: FormControl;
  platformTypes: FormControl;
  genres: FormControl;
  description: FormControl;
  dropdownList = [];
  dropdownListGenres = [];

  constructor(protected spinner: NgxSpinnerService,
    protected route: ActivatedRoute,
    protected gameService: GameService,
    protected router: Router) {
  }

  selectedItems = [];
  selectedItemsGenre = [];
  onSelectAllIsTriggered = false;
  onItemSelectGenreIsTriggered = false;

  onChangePlatformTypes(event) {
    if (this.selectedItems === event) return;
    this.selectedItems = event;
    this.onSelectAllIsTriggered = true;
  }

  onChangeGenres(event) {
    if (this.selectedItemsGenre === event) return;
    this.selectedItemsGenre = event;
    this.onItemSelectGenreIsTriggered = true;
  }
  dropdownSettings = {
    singleSelection: false,
    idField: 'item_id',
    textField: 'item_text',
    selectAllText: 'Select All',
    unSelectAllText: 'UnSelect All',
    itemsShowLimit: 3,
    allowSearchFilter: true
  };

  ngAfterViewInit(): void {
    this.fillExistInfo();
  }

  ngOnInit(): void {
    this.spinner.show();
    this.name = new FormControl(null, Validators.required);
    this.platformTypes = new FormControl(null, Validators.required);
    this.genres = new FormControl(null, Validators.required);
    this.description =
      new FormControl(null, [Validators.required, Validators.maxLength(400)]);

    this.gameForm = new FormGroup({
      name: this.name,
      platformTypes: this.platformTypes,
      genres: this.genres,
      description: this.description
    });
  }

  validateForm() {
    return !this.gameForm.invalid && (this.description.dirty || this.name.dirty || this.onSelectAllIsTriggered || this.onItemSelectGenreIsTriggered);
  }

  cancel() {
    this.router.navigate(['/games']);
  }


}
