import { DeleteService } from "./delete.service"
import { GetService } from "./get.service"
import { PostService } from "./post.service"
import { PutService } from "./put.service"

export let services = [GetService, PostService, PutService, DeleteService]