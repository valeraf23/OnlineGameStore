import { ActivatedRouteSnapshot, CanDeactivate } from '@angular/router';
import { Injectable } from '@angular/core';
import { CanComponentDeactivate } from './can-deactivate-component.interface';

@Injectable()
export class CanDeactivateGuard implements CanDeactivate<CanComponentDeactivate> {

  public async canDeactivate(component: CanComponentDeactivate, currentRoute: ActivatedRouteSnapshot): Promise<boolean> {
    return await component.canDeactivate ? component.canDeactivate(currentRoute) : true;
  }
}


