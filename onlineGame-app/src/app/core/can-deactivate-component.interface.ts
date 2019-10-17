import { ActivatedRouteSnapshot } from '@angular/router';

export interface CanComponentDeactivate {
  canDeactivate: (currentRoute?: ActivatedRouteSnapshot) => Promise<boolean>;
}
