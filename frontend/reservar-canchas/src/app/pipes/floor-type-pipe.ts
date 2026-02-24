import { Pipe, PipeTransform } from '@angular/core';
import { FloorType } from '../models/field/floortype.enum';

@Pipe({
  name: 'floorTypeText'
})
export class FloorTypePipe implements PipeTransform {

    transform(value: FloorType | string): string {
      switch (value) {
            case FloorType.CespedNatural:
              return 'Cesped Natural';
            case FloorType.CespedSintetico:
              return 'Cesped Sintético';
            case FloorType.Parquet:
              return 'Parquet';
            case FloorType.Cemento:
              return 'Cemento';
            default:
              return value; 
          }
    }

}
