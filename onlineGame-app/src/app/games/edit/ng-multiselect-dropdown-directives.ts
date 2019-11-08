import { ElementRef, Directive, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appErrorHighlight]'
})
export class ErrorHighlightDirective {
  constructor(private element: ElementRef, private renderer: Renderer2) {}
  @Input() set appErrorHighlight(condition: string) {

    debugger
    if (condition ==='true') {
      this.renderer.addClass(this.element.nativeElement.querySelector('.multiselect-dropdown'), 'border');
      this.renderer.addClass(this.element.nativeElement.querySelector('.multiselect-dropdown'), 'border-danger');
    } else {
      this.renderer.removeClass(this.element.nativeElement.querySelector('.multiselect-dropdown'), 'border');
      this.renderer.removeClass(this.element.nativeElement.querySelector('.multiselect-dropdown'), 'border-danger');
    }
  }
}
