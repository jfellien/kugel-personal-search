import { Component, Input, OnInit } from '@angular/core';
import { Option } from '../../models/option.model';
import { SelectorService } from './selector.service';

@Component({
  selector: 'app-selector',
  templateUrl: './selector.component.html',
  styleUrls: ['./selector.component.scss']
})
export class SelectorComponent implements OnInit {

  @Input() path: string | undefined;

  errorMessage: string | undefined;
  areas: Option[] | undefined;

  constructor(private selectorService: SelectorService) { }

  ngOnInit(): void {
    if (!this.path) {return;}
    this.errorMessage = undefined;
    this.selectorService.get(this.path).subscribe(result => {
      this.areas = result;
    }, () => {
      this.errorMessage = 'Laden der Produktbereiche fehlgeschlagen.';
    });
  }

}
