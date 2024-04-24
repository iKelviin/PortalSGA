import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-input-select',
  templateUrl: './input-select.component.html',
  styleUrl: './input-select.component.scss'
})
export class InputSelectComponent implements OnInit{
  @Input() options: any[];
  @Input() width: string;
  outWidth: string;

ngOnInit(): void {
  if(this.width.includes("%")) {

    
    this.outWidth = this.width+'%';
  }
  else {
    this.outWidth = this.width + 'px';
  }
}

  constructor() {}
}
