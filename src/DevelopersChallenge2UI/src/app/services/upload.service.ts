import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UploadService {

  constructor(private http: HttpClient) { }

  public uploadFile(files: FileList) {
    let formData = new FormData();

    for (let i = 0; i < files.length; i++) {
      formData.append('file', files[i], files[i].name);
    }

    const request = new HttpRequest('POST', `${environment.baseUrl}/api/upload`, formData);
    
    return this.http.request(request);
    // return this.http.post<any>(`${environment.baseUrl}/api/upload`, formData, {
    //   reportProgress: true,
    //   observe: 'events'
    // });
  }
}
