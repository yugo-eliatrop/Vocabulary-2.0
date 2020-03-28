import "../scss/index.scss";

import TaskHandler from "./TaskHandler";
import StatisticHandler from "./StatisticHandler";
import ControlPanel from "./ControlPanel";

if (window.location.pathname === "/Learning/Index") {
  window.taskHandler = new TaskHandler();
  window.controlPanel = new ControlPanel(window.taskHandler);
}

window.statisticHandler = new StatisticHandler();
