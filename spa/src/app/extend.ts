export interface IGrouped<TItem, TKey> {
    key: TKey;
    items: TItem[];
}

export type ReduceFn<TItem, TKey> = (result: IGrouped<TItem, TKey>[], item: TItem) => IGrouped<TItem, TKey>[];

export class Extensions {
    public static sortBy<T>(key: (o: T) => number | string | Date | boolean, desc?: boolean): (a: T, b: T) => number {
        return (a: T, b: T): number => {

            const m: number = desc ? -1 : 1;

            const keyA: number | string | Date | boolean = key(a);
            const keyB: number | string | Date | boolean = key(b);

            if (keyA > keyB) {
                return m;
            }
            if (keyA < keyB) {
                return -1 * m;
            }
            return 0;
        };
    }

    public static groupBy<TItem, TKey>(keyFn: (o: TItem) => TKey): ReduceFn<TItem, TKey> {
        return (result: IGrouped<TItem, TKey>[], item: TItem): IGrouped<TItem, TKey>[] => {

            const key: TKey = keyFn(item);
            let i: IGrouped<TItem, TKey> = result.find((v: IGrouped<TItem, TKey>) => v.key === key);
            if (!i) {
                i = {
                    key, items: []
                };

                result.push(i);
            }

            i.items.push(item);

            return result;
        };
    }
}
