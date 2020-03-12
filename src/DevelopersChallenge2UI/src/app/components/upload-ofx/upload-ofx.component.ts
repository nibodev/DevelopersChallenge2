import { Component, OnInit } from '@angular/core';
import { UploadService } from 'src/app/services/upload.service';

@Component({
  selector: 'app-upload-ofx',
  templateUrl: './upload-ofx.component.html',
  styleUrls: ['./upload-ofx.component.css']
})
export class UploadOfxComponent implements OnInit {

  public listFiles: FileList;

  constructor(private uploadService: UploadService) {}

  ngOnInit(): void {
  }

  /**
   * uploadFiles
   * Send the files to server
   */
  public uploadFiles(): void {
    this.uploadService.uploadFile(this.listFiles)
    .subscribe(
      res => console.log('Arquivos carregados com sucesso: ', res),
      err => console.error('Falha ao carregar os arquivos! ', err)
    );
  }

  /**
   * loadFiles
   * Load the files to show in the UI
   */
  public loadFiles(files: FileList): void {
    this.listFiles = files;

    const filesNames = [];

    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < files.length; i++) {
      filesNames.push(files[i].name);
    }

    document.getElementById('inputGroupFile01Label').innerText = filesNames.join(', ');
  }
}
