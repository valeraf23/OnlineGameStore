import { Component, OnInit } from '@angular/core'
import { FormControl, FormGroup, Validators } from '@angular/forms'
import { GameService } from "../game.service";
import { Router } from '@angular/router'

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

export class GameDetailComponent implements OnInit {
  newGameForm: FormGroup;
  name: FormControl;
  platformTypes: FormControl;
  genres: FormControl;
  description: FormControl;
  dropdownList = [];
  dropdownListGenres = [];
  constructor(private gameService: GameService, private router: Router) { }

  selectedItems = new Set();
  selectedItemsGenre = new Set();
  onItemSelect(item: any) {
    this.selectedItems.add(item);
  }
  onSelectAll(items: any) {
    this.selectedItems.add(items);
  }

  onItemSelectGenre(item: any) {       
      this.selectedItemsGenre.add(item);
  }
  onSelectAllGenre(items: any) {
    items.map(x => this.onItemSelectGenre(x));
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

  ngOnInit() {
    this.gameService.getPlatformTypes().subscribe(
      pt => {
        debugger;
        this.dropdownList = pt.map((x) => ({
          item_id: x.id,
          item_text: x.type 

        }));      
      }
    );
    this.gameService.getGenres().subscribe(
      pt => {
        debugger;
        this.dropdownListGenres = pt.filter(y=>y.parentGenre===null).map((x) => ({
          item_id: x.id,
          item_text: x.name
        }));
      }
    );
    this.name = new FormControl('', Validators.required);
    this.platformTypes = new FormControl('', Validators.required);
    this.genres = new FormControl('', Validators.required);
    this.description =
      new FormControl('', [Validators.required, Validators.maxLength(400)]);

    this.newGameForm = new FormGroup({
      name: this.name,
      platformTypes: this.platformTypes,
      genres: this.genres,
      description: this.description
    });
  }

  saveGame(formValues) {
    let session = {
      name: formValues.name,
      description: formValues.description,
      publisherId: undefined,
      genresId: [...this.selectedItemsGenre].map(x => (x.item_id)),
      platformTypesId: [...this.selectedItems].map(x => (x.item_id))
    }
    debugger;
    if (this.newGameForm.valid) {
      this.gameService.postGame(JSON.stringify(session)).subscribe();
      this.router.navigate(['/games']);
    }
  
  }

  cancel() {
    this.router.navigate(['/games']);
  }
}
