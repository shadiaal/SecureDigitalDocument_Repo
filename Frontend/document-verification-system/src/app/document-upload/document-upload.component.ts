
import { CommonModule } from '@angular/common'; 
import { Component } from '@angular/core';
import { DocumentService } from '../services/document.service';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-document-upload',
  templateUrl: './document-upload.component.html',
  styleUrls: ['./document-upload.component.css'],
  imports:[CommonModule,FormsModule]
})
export class DocumentUploadComponent {
  title: string = '';
  file: File | null = null;
  loading: boolean = false;
  errorMessage: string | null = null;

  constructor(private documentService: DocumentService) {}

  onFileChange(event: any): void {
    this.file = event.target.files[0];
  }

  
  onSubmit(): void {
    if (!this.title || !this.file) {
      this.errorMessage = 'Please fill out all fields.';
      return;
    }

    const formData = new FormData();
    formData.append('title', this.title);
    formData.append('file', this.file);

    this.loading = true;
    this.documentService.uploadDocument(formData).subscribe(
      (response) => {
        this.loading = false;
        console.log('Document uploaded successfully', response);
      },
      (error) => {
        this.loading = false;
        this.errorMessage = 'Failed to upload document.';
        console.error('Error uploading document', error);
      }
    );
  }
}
