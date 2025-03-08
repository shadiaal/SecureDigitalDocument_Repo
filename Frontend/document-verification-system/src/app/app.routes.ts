
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DocumentUploadComponent } from './document-upload/document-upload.component';
import { VerificationComponent } from './verification/verification.component';

export const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'upload', component: DocumentUploadComponent },
  { path: 'verify', component: VerificationComponent },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
