const formatter = Intl.NumberFormat("en-us", {
    style: "currency",
    currency: "USD"
});

export function formatMoney(value) {
    if (typeof value === "number") {
        return formatter.format(value);
    }
    return value;
}