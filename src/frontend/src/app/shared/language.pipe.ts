import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'language'
})
export class LanguagePipe implements PipeTransform {

  transform(value: string | undefined, ...args: unknown[]): string {
    switch(value) {
      case 'de': 
        return 'Deutsch';
      case 'en':
        return 'Englisch';
      case 'fr':
        return 'Französisch';
      default:
        return value ? value : '';
    }
  }

}
