class MapArray<T> {
    items:T[];
    map: Map<string, T>;
    push(item:T, id:string){
        this.items.push(item)
        this.map.set(id, item)
    }
    get(id:string):T|undefined{
        return this.map.get(id)
    }
    remove(id:string){
        let item = this.get(id)
        this.items = this.items.filter(i => i != item)
        this.map.delete(id)
    }
}