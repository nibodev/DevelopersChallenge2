import { BankList } from './../../Models/BankList';
import { Component, OnInit } from '@angular/core';
import { ListOfxService } from 'src/app/services/list-ofx.service';

@Component({
  selector: 'app-list-ofx',
  templateUrl: './list-ofx.component.html',
  styleUrls: ['./list-ofx.component.css']
})
export class ListOfxComponent implements OnInit {
  public listTransaction: any;

  constructor(private listOfx: ListOfxService) {}

  ngOnInit(): void {
    this.loadList();
  }

  public loadList(): void {
    this.listOfx.getTransactions()
    .subscribe(
      res => {
        console.log('Arquivos carregados com sucesso: ', res);
        console.log(`Bank List: ${res['body']}`);
        this.listTransaction = res[0];
      },
      err => console.error('Falha ao carregar os arquivos! ', err)
    );
  }
}
