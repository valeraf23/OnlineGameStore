import { Component, OnInit } from '@angular/core'
import { FormControl, FormGroup, Validators } from '@angular/forms'
import { GameService } from "../game.service";
import { Router } from '@angular/router';
import { ActivatedRoute } from "@angular/router";
import { IGame } from "../gameModel";

@Component({
  templateUrl: './edit-detail.component.html',
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
export class GameEditComponent implements OnInit {
  editForm: FormGroup;
  name: FormControl;
  platformTypes: FormControl;
  genres: FormControl;
  description: FormControl;
  dropdownList = [];
  dropdownListGenres = [];
  id: string;
  game :IGame;
  constructor(private route: ActivatedRoute, private gameService: GameService, private router: Router) { }

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
    this.route.paramMap.subscribe(params => {
      this.id = params.get("id")
    })
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

    this.gameService.getGame(this.id).subscribe(
      pt => {
        this.game = pt;
        debugger        
        this.selectedItems = new Set([...pt.platformTypes]);
        this.selectedItemsGenre = new Set([...pt.genres]);

        this.name.setValue(pt.name);
        this.description.setValue(pt.description);
      }
    );

    debugger;
    this.name = new FormControl(null, Validators.required);
    this.platformTypes = new FormControl(null, Validators.required);
    this.genres = new FormControl(null, Validators.required);
    this.description =
      new FormControl(null, [Validators.required, Validators.maxLength(400)]);
    debugger;
    this.editForm = new FormGroup({
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
    if (this.editForm.valid) {
      this.gameService.putGame(this.id,JSON.stringify(session)).subscribe();
      this.router.navigate(['/games']);
    }
  
  }

  cancel() {
    this.router.navigate(['/games']);
  }
}
