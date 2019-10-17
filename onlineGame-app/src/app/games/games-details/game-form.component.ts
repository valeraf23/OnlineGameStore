import { OnInit, AfterViewInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GameService } from '../../core/game.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ConfirmationDialogService } from '../confirmation-dialog/confirmation-dialog.service';
import { CanComponentDeactivate } from 'src/app/core/can-deactivate-component.interface';

export abstract class BaseGameFormComponent
  implements OnInit, AfterViewInit, CanComponentDeactivate {
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

  abstract fillExistInfo(): void;
  abstract saveGame(formValues: any): void;

  constructor(
    protected spinner: NgxSpinnerService,
    protected route: ActivatedRoute,
    protected gameService: GameService,
    protected router: Router,
    protected confirmationDialogService: ConfirmationDialogService
  ) {}

  onChangePlatformTypes(event: []) {
    if (this.selectedItems === event) {
      return;
    }
    this.selectedItems = event;
    this.onSelectAllIsTriggered = true;
  }

  onChangeGenres(event) {
    if (this.selectedItemsGenre === event) {
      return;
    }
    this.selectedItemsGenre = event;
    this.onItemSelectGenreIsTriggered = true;
  }

  ngAfterViewInit(): void {
    this.fillExistInfo();
  }

  ngOnInit(): void {
    this.spinner.show();
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

  validateForm() {
    return (
      this.description.dirty ||
      this.name.dirty ||
      this.onSelectAllIsTriggered ||
      this.onItemSelectGenreIsTriggered
    );
  }

  protected markAsPristine(): void {
    this.gameForm.markAsPristine();
    this.onSelectAllIsTriggered = false;
    this.onItemSelectGenreIsTriggered = false;
  }

  cancel() {
    this.router.navigate(['/games']);
  }

  async canDeactivate(): Promise<boolean> {
    if (this.validateForm()) {
      return await this.openConfirmationDialog();
    }

    return true;
  }

  protected async openConfirmationDialog(): Promise<boolean> {
    try {
      const confirmed = await this.confirmationDialogService.confirm(
        'Please confirm..',
        'Leaving this page will lose your changes. Are you sure want to leave this page ?'
      );
      if (confirmed) {
      }
      return confirmed;
    } catch (e) {
      console.log(e);
      return false;
    }
  }
}
