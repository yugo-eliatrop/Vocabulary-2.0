import TaskHandler from "./TaskHandler";
import StatisticHandler from "./StatisticHandler";

if (window.location.pathname === "/Learning/Index")
  window.taskHandler = new TaskHandler();

window.statisticHandler = new StatisticHandler();
