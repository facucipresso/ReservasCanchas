import { Pipe, PipeTransform } from '@angular/core';
import { FieldType } from '../models/field/fieldtype.enum';

@Pipe({
  name: 'fieldTypeText'
})
export class FieldTypePipe implements PipeTransform {

  transform(value: FieldType | string): string {
    switch (value) {
          case FieldType.Futbol5:
            return 'Futbol 5';
          case FieldType.Futbol7:
            return 'Futbol 7';
          case FieldType.Futbol11:
            return 'Futbol 11';
          default:
            return value; 
        }
  }

}
