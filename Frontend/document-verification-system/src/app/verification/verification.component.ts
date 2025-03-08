import { Component } from '@angular/core';
import { DocumentService } from '../services/document.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; 
@Component({
  selector: 'app-verification',
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.css'],
   imports:[CommonModule,FormsModule]
})
export class VerificationComponent {
  verificationCode: string = '';
  verificationResult: string = '';

  constructor(private documentService: DocumentService) {}

  onVerify(): void {
    this.documentService.verifyDocument(this.verificationCode).subscribe(
      (response) => {
        this.verificationResult = 'Verification successful!';
      },
      (error) => {
        this.verificationResult = 'Verification failed!';
        console.error('Error verifying document', error);
      }
    );
  }
}

