import { HttpEvent, HttpEventType, HttpResponse } from "@angular/common/http";
import { Observable, pipe } from "rxjs";
import { filter, map, tap } from "rxjs/operators";

export function filterResponse<T>(){
    return pipe(
        filter((event: HttpEvent<T>) => event.type === HttpEventType.Response),
        map((event: HttpEvent<T>) => {
            if (event instanceof HttpResponse) {
                return event.body;
            }
            // Se não for uma instância de HttpResponse, retorne null ou trate de outra forma
            return null;
        })
    );
}


export function uploadProgress<T>(cb: (progress: number) => void){
    return tap((event: HttpEvent<T>) => {
        if(event.type === HttpEventType.UploadProgress  && event.total !== undefined){
            cb(Math.round((event.loaded * 100) / event.total));
        }
    });

}