import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-collapsible-selector',
  templateUrl: './collapsible.component.html',
  styleUrls: ['./collapsible.component.css']
})
export class CollapsibleComponent {
  visible = false;
  visibleAnswer = false;

  @Input()
  textOn: string;

  @Input()
  textOff: string;

  toggleContent() {
    this.visible = !this.visible;
  }
}
