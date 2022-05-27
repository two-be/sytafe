import { ApiRoute, UserInfo } from "./models"

declare let process: any

export let getApi = (route: ApiRoute, path?: string) => {
    let baseAddress = ""
    if (isDevelopment) {
        baseAddress = "http://localhost:5077"
    }
    return `${baseAddress}/${route}/${path || ""}`
}

export let getUser = () => {
    try {
        let json = localStorage.getItem("user")
        if (json) {
            let user = JSON.parse(json)
            return user
        }
        return new UserInfo()
    } catch (err) {
        return new UserInfo()
    }
}

export let isDevelopment = process.env.NODE_ENV == "development"

export let log = (value) => {
    if (isDevelopment) {
        console.log(value)
    }
}