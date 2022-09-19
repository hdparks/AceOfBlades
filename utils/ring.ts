export function ring<T>(arr:Array<T>, startingIdx:number):Array<T> {
    return [...arr.slice(startingIdx), ...arr.slice(0,startingIdx)]
}