import { Component, OnInit } from '@angular/core';
import { BankList } from 'src/app/Models/BankList';
import { Transaction } from 'src/app/Models/Transaction';

@Component({
  selector: 'app-list-ofx',
  templateUrl: './list-ofx.component.html',
  styleUrls: ['./list-ofx.component.css']
})
export class ListOfxComponent implements OnInit {
  public listTransaction: BankList;

  constructor() {
    this.mockTest();
   }

  ngOnInit(): void {
  }

  public mockTest(): void {
    this.listTransaction = new BankList();
    this.listTransaction.dateStart = new Date('2014-02-01');
    this.listTransaction.dateEnd = new Date('2014-02-01');
    this.listTransaction.transactions = new Array<Transaction>();

    let mock = new Transaction();
    mock.datePosted = new Date('2014-02-03');
    mock.transactionType = 'Debit';
    mock.transactionAmount = -140.00;
    mock.memo = 'CXE     001958 SAQUE';
    this.listTransaction.transactions.push(mock);
  }
}
