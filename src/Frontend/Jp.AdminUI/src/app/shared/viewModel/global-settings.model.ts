export class GlobalSettings {
    key: string;
    value: string;
    isPublic: boolean;
    isSensitive: boolean;

    public static getSetting(model: Array<GlobalSettings>, key: string): GlobalSettings {
        let item = model.find(f => f.key === key);
        if (item.value == null)
            item.value = "";

        return item;
    }

    public static updateSetting(model: Array<GlobalSettings>, key: string, value: string): GlobalSettings {
        let item = model.find(f => f.key === key);
        if (item != null)
            item.value = value;

        return item;
    }
}