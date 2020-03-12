import { BankList } from './../Models/BankList';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ListOfxService {

  constructor(private http: HttpClient) { }

  public getTransactions() {
    return this.http.get(`${environment.baseUrl}/api/upload`);
  }
}
