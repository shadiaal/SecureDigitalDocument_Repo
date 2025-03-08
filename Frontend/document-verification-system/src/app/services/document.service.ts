
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  private baseUrl = 'http://localhost:5293/api/documents'; // API URL

  constructor(private http: HttpClient) {}

  uploadDocument(data: FormData): Observable<any> {
    return this.http.post(`${this.baseUrl}`, data);  
  }
  getDocumentById(id: number): Observable<Document> {
    return this.http.get<Document>(`${this.baseUrl}/${id}`);
  }
  getAllDocuments(): Observable<Document[]> {
    return this.http.get<Document[]>(this.baseUrl);
  }
  verifyDocument(code: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/verify`, { verificationCode: code });
  }
}

