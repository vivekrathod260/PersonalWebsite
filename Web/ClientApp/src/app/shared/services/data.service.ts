import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class DataService {
    constructor(private httpClient: HttpClient)
    { }

    baseUrl = "https://localhost:44367/";

    get<T>(apiUrl: string) {
        return this.httpClient.get<T>(this.buildUrl(apiUrl), { headers: this.getDefaultHeaders() });
    }

    post<T>(apiUrl: string, data: any) {
        return this.httpClient.post<T>(this.buildUrl(apiUrl), JSON.stringify(data), { headers: this.getDefaultHeaders() });
    }

    put<T>(apiUrl: string, data: any) {
        return this.httpClient.put<T>(this.buildUrl(apiUrl), JSON.stringify(data), { headers: this.getDefaultHeaders() });
    }

    delete<T>(apiUrl: string) {
        return this.httpClient.delete<T>(this.buildUrl(apiUrl), { headers: this.getDefaultHeaders() });
    }

    patch<T>(apiUrl: string, data: any) {
        return this.httpClient.patch<T>(this.buildUrl(apiUrl), data, { headers: this.getDefaultHeaders() });
    }

    private getDefaultHeaders(accept: string = "application/json") : HttpHeaders
    {
          return new HttpHeaders()
              .append("Content-Type", accept || "application/json")
              .append("accept","application/json")
              .append("Cache-Control", "no-store")
              .append("Pragma", "no-cache")
              .append("Expires", "-1")
              .append("If-Modified-Since", "0");
    }

    private buildUrl(apiUrl: string) {  
        return `${this.baseUrl}${apiUrl}`;
    }
}
