import { Component } from '@angular/core';
import { UploadService } from './services/upload.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'DevelopersChallenge2UI';

  public listFiles: FileList;

  constructor(private uploadService: UploadService) {}

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
  public loadFiles(files:FileList): void {
    this.listFiles = files;

    const filesNames = [];
    for (let i = 0; i < files.length; i++) {
      filesNames.push(files[i].name);
    }

    document.getElementById('inputGroupFile01Label').innerText = filesNames.join(', ');
  }
}
