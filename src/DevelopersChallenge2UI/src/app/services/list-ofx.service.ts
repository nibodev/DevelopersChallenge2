import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ListOfxService {

  constructor(private http: HttpClient) { }

  public getTransactions(): Array<any> {
    return this.http.get(`${environment.baseUrl}/api/upload`);
  }
}
