import { IConfig } from "../../Interfaces/IConfig";
import { ObjectInitializer } from "../../Models/ObjectInitializer";
import { HttpRequestManager, MethodType } from "./HttpRequestManager";

export abstract class BackendServiceBase<T> {
    protected readonly config: IConfig;
    protected readonly objInitializer: ObjectInitializer<T>;
    private readonly _requestManager: HttpRequestManager;

    constructor(constructorFunc: { new (obj: T): T }) {
        this.objInitializer = new ObjectInitializer<T>(constructorFunc);
        this.config = {
            backendUrl: process.env.REACT_APP_BACKEND_URL
        };
        this._requestManager = new HttpRequestManager(this.config.backendUrl, this._getController());
    }

    protected abstract getConstructorName() : string;

    public async getTitles() : Promise<T[]> {
        const response = await this._requestManager.send(MethodType.GET, "gettitles");
        const result = await response.json() as T[];

        return this.objInitializer.getInicializedObjects(result);
    }

    private _getController() : string {
        const serviceName = this.getConstructorName();
        return serviceName.replace("BackendService", "");
    }
}