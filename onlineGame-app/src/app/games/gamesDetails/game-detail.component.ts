import { Component, OnInit } from '@angular/core';
import { IGame} from '../gameModel';

@Component({
  templateUrl: './game-detail.component.html',
  styleUrls: ['./game-detail.component.css']
})
export class GameDetailComponent implements OnInit {
  pageTitle: string = 'Game Detail';
  game: IGame;

  ngOnInit() {
  }

}
