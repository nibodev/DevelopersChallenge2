import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-list-ofx',
  templateUrl: './list-ofx.component.html',
  styleUrls: ['./list-ofx.component.css']
})
export class ListOfxComponent implements OnInit {
  public listTransaction = [];

  constructor() { }

  ngOnInit(): void {
  }

}
