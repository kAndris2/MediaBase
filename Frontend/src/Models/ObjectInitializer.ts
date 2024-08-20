export class ObjectInitializer<T> {
    private readonly constructorFunc: { new (obj: T): T };

    constructor(constructorFunc: { new (obj: T): T }) {
        this.constructorFunc = constructorFunc;
    }

    public getInicializedObjects(rawObjects: T[]) : T[] {
        return rawObjects.map(this.getInicializedObject.bind(this));
    }

    public getInicializedObject(rawObject: T) : T {
        return new this.constructorFunc(rawObject);
    }
}