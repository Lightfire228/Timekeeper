import { invoke } from "@tauri-apps/api/core";
import type { Task, TaskInput } from "./types/task";

export async function test_db() {
    await invoke("test_db", {});
}

export async function print_db() {
    await invoke("print_db", {});
}

export async function new_task(task: TaskInput) {
    await invoke("new_task", { task });
}

export async function delete_task(taskId: number) {
    await invoke("delete_task", { taskId });
}

export async function get_tasks(): Promise<Array<Task>> {
    return await invoke("get_tasks", {});
}

export async function backup_db() {
    return await invoke("backup_db", {});
}
