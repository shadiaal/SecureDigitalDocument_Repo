import { createReducer, on } from '@ngrx/store';
import { loadDocuments, loadDocumentsSuccess, loadDocumentsFailure } from './document.actions';

export interface DocumentState {
  documents: any[];
  loading: boolean;
  error: string | null;
}

export interface RootState {  
  document: DocumentState;
}

const initialState: DocumentState = {
  documents: [],
  loading: false,
  error: null
};

export const documentReducer = createReducer(
  initialState,
  on(loadDocuments, (state) => ({ ...state, loading: true })),
  on(loadDocumentsSuccess, (state, { documents }) => ({ ...state, loading: false, documents })),
  on(loadDocumentsFailure, (state, { error }) => ({ ...state, loading: false, error }))
);

