
import { CommonModule } from '@angular/common'; 
import { Component, OnInit } from '@angular/core';
import { DocumentService } from '../services/document.service';

@Component({
  selector: 'app-dashboard',
  imports:[CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  documents: any[] = [];

  constructor(private documentService: DocumentService) {}

  ngOnInit(): void {
    this.loadDocuments();
  }

  loadDocuments(): void {
    this.documentService.getAllDocuments().subscribe(
      (response) => {
        this.documents = response;
      },
      (error) => {
        console.error('Error fetching documents', error);
      }
    );
  }
}

