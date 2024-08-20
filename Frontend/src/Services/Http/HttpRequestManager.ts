export enum MethodType {
    GET = "GET",
}

export class HttpRequestManager {
    private readonly _backendUrl: string;
    private readonly _controller: string;

    constructor(backendUrl: string, controller: string) {
        this._backendUrl = backendUrl;
        this._controller = controller;
    }

    public async send(method: MethodType, endPoint: string, params: {[key: string]: string | number} = null, obj: Object = null) : Promise<Response> {
        const requestParts = [
            this._backendUrl, "api", this._controller, endPoint
        ];
        const requestUrl = requestParts.join('/') + this._createQueryStr(params);

        try {
            switch (method) {
                case MethodType.GET: 
                    return await this._get(requestUrl);
    
                default: 
                    throw new Error("Unsupported method type!");
            }
        }
        catch (ex) {
            throw new Error(`An error occurred while we tried to reach the backend! Ex.: ${ex.message}`);
        }
    }

    private async _get(requestUrl: string) : Promise<Response> {
        return await fetch(requestUrl);
    }

    private _createQueryStr(params: {[key: string]: string | number}) : string {
        const pairs: string[] = [];

        for (const [key, value] of this._convertToMap(params)) {
            pairs.push(`${key}=${value}`);
        }

        if (pairs.length == 0) return "";

        return `?${pairs.join('&')}`;
    }

    private _convertToMap(data: {[key: string]: string | number}) : Map<string, string> {
        const dictionary = new Map<string, string>();

        for (let key in data) {
            dictionary.set(key, data[key] as string)
        }

        return dictionary;
    }
}