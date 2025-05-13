export type FilterParams<T> = Pick<T, Extract<keyof T, `Filter.${string}`>>;
