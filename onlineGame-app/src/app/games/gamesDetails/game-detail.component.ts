import { Component, OnInit } from '@angular/core';
import { IGame } from '../gameModel';
import { ActivatedRoute, Router } from '@angular/router';
import { System } from '../gameModel';


@Component({
  templateUrl: './game-detail.component.html',

})
export class GameDetailComponent implements OnInit {
  pageTitle: string = 'Game Detail';
  game: IGame;

  constructor(private route: ActivatedRoute,
    private router: Router) {
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    this.pageTitle += `: ${id}`;
    this.game = {
      'id': new System.Guid(id),
      'name': 'Leaf Rake',
      'description': 'GDN-0011',
      'publisher': null,
      'comments': null,
     'genres': null,
      'platformTypes': null,

     
    }
  }
  onBack(): void {
    this.router.navigate(['/games']);
  }
}

