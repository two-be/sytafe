class Abstract {
    id = ""
}

export type ApiRoute = "screenTime" | "used" | "user"

export type DayOfWeek = "Sunday" | "Monday" | "Tuesday" | "Wednesday" | "Thursday" | "Friday" | "Saturday"

export class ScreenTimeInfo extends Abstract {
    anytime = false
    availableFrom: any = new Date()
    availableTo: any = new Date()
    dayOfWeek: DayOfWeek
    minuteLimit = 0
    userId = ""
}

export class SelectOptionInfo {
    label = ""
    value: any
}

export class UsedInfo extends Abstract {
    dayOfWeek = ""
    displayFrom = ""
    displayTo = ""
    from: any = new Date()
    to: any = new Date()
}

export class UserInfo extends Abstract {
    isAdministrator = false
    password = ""
    username = ""
    type = ""
}