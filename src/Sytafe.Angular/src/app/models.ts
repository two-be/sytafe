class Abstract {
    id = ""
}

export type ApiRoute = "screenTime" | "user"

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

export class UserInfo extends Abstract {
    password = ""
    username = ""
    type = ""
}