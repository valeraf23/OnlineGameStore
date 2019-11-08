import { AbstractControl, ValidatorFn } from '@angular/forms';

export class NgMultiselectValidators {
  static validateNgMultiselect(): ValidatorFn {
    return (c: AbstractControl): { [key: string]: boolean } | null => {
      if (c.value !== null && (c.value as Array<any>).length === 0) {
        return { required: true };
      }
      return null;
    };
  }
}
