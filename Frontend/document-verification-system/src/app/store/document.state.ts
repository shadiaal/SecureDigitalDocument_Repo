export interface DocumentState {
    documents: any[];
    loading: boolean;
    error: string | null;
  }
  
  export const initialState: DocumentState = {
    documents: [],
    loading: false,
    error: null,
  };
  