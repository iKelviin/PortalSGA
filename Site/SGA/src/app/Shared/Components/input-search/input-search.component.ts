import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-input-search',
  templateUrl: './input-search.component.html',
  styleUrl: './input-search.component.scss'
})
export class InputSearchComponent implements OnInit{
  @Input() placeholder: string = 'Example placeholder';
  @Input() type: string = 'text';
  @Input() width: string;  // Este valor pode ser algo como '50%', '200px', etc.
  outWidth: string;

  ngOnInit(): void {
    // Verifica se a largura inclui uma unidade conhecida
    if (this.width.match(/(px|%|em|rem)$/)) {
      this.outWidth = this.width;
    } else {
      this.outWidth = this.width + 'px';  // Assumir px se nenhuma unidade especificada
    }
  }
}
