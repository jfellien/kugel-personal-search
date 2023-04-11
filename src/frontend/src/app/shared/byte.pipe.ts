import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'byte'
})
export class BytePipe implements PipeTransform {

  transform(value: number | undefined, base: 'byte' | 'kilobyte' = 'kilobyte' ): string {
    if (!value) {return '';}

    if (base === 'kilobyte') {
      if (value > 1048576) {
        return Math.round((value / 1024 / 1024)) + ' GB';
      } else if (value > 1024) {
        return Math.round((value / 1024)) + ' MB';
      } else {
        return Math.round(value) + ' KB';
      }
    } else {
      if (value > 1073741824) {
        return Math.round((value / 1024 / 1024 / 1024)) + ' GB';
      } else if (value > 1048576) {
        return Math.round((value / 1024 / 1024)) + ' MB';
      } else if (value > 1024) {
        return Math.round((value / 1024)) + ' KB';
      } else {
        return Math.round(value) + ' B';
      }
    }
  }

}
