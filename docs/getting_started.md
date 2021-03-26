---
layout: default
title: Getting started
---

# Getting started with **DG-Sim**

## Quickstart Tutorial

This quickstart tutorial will introduce you to basics of working with DG-Sim. The steps we will lead you through include:
<br>
* Installing a Packages
* Creating a new library
* Viewing and editing model Datafeeds
* Running a Model
* Analyzing the Results

## **Step 1: Install DG-Sim**
**DG-Sim** is a module within the [SyncroSim](https://syncrosim.com/) simulation modeling framework; as such, running **DG-Sim** requires that the **SyncroSim** software be installed on your computer.
1. To install **DG-Sim**, follow the instructions under [How to Install](https://apexrms.github.io/dgsim/)
2. Once installed, open the **SyncroSim** Windows application.

## **Step 2: Create a new DG-Sim library**
Having installed the **DG-Sim** package, you are now ready to create your first SyncroSim Library. A Library is a file (with extension *.ssim*) that contains all of your model inputs and outputs. Note that the format of each Library is specific to the Package for which it was initially created. To create a new Library, choose **New Library...** from the **File** menu.
<br>
<img align="middle" style="padding: 3px" width="700" src="assets/images/screencap-1.PNG">
<br>
In this window:
<br>
* Select the row for the ***dgsim - Simulates demographics of wildlife populations***. Note that as you select a row, the list of **Templates** available and suggested file **Name** for that base package are updated.
* Select the ***Simple Pop. Model*** Template as shown above.
* Optionally type in a new **Name** for the Library (or accept the default); you can also change the **Location** of the file using the **Browse...** button.
<br>
When you are ready to create the Library file, click **OK**. A new Library will be created and loaded into the Library Explorer.

## **Step 3: Review the model inputs**
The contents of your newly created Library are now displayed in the Library Explorer. Model inputs in SyncroSim are organized into Scenarios, where each Scenario consists of a suite of values, one for each of the Model's required inputs.

**Note:** Because you chose the ***Simple Pop. Model*** Template when you created your Library, your Library already contains two pre-configured Scenarios with model inputs. These inputs were filled in and distributed as a sample with the package to help you get started quickly, and represent hypothetical management Scenarios: one a Baseline, and another at 4x Harvest.
<br>
<img align="middle" style="padding: 3px" width="550" src="assets/images/screencap-2.PNG">
<br>
As shown in the image above, the Library you have just opened contains two Scenarios, each with a unique ID. The first of these scenarios (with ID=1, as shown above in square brackets) is named ***Baseline Scenario***; this scenario contains a suite of model inputs corresponding to a hypothetical baseline harvest plan. The second scenario (with ID=10 and named ***4x Harvest Scenario***) contains model inputs corresponding to an alternative plan where harvest targets are 4 times greater than those of the Baseline Scenario.
<br>
To view the details of the first of these Scenarios:
<br>
* Select the scenario named ***Baseline Scenario*** in the Library Explorer.
* Right-click and choose **Properties** from the context menu to view the details of the Scenario.

This opens the Scenario Properties window.
<br>
<img align="middle" style="padding: 3px" width="600" src="assets/images/screencap-3.PNG">
<br>
The first tab in this window, called **Summary**, displays some generic information for the Scenario. The second tab, **Run Control**, contains parameters for running a model simulation. In this example, the Scenario will run for 20 years, starting in the year 2018 on Julian day 152 (June 1st), and repeated for 100 Monte Carlo iterations. The Start Julian day represents the date at which the simulation begins each year, as well as the parturition (i.e. birth) date for females, and the date at which model output is reported.
<br>
<img align="middle" style="padding: 3px" width="600" src="assets/images/screencap-4.png">
<br>
Click on the **Initial Population**, **Demographic Parameters**, and **Harvest** tabs to familiarize yourself with this Scenario's inputs. By default, the harvest is specified as a number of individuals harvested per year (although it can also be specified as a rate using the **Options** menu on the left). Between 2018 and 2025 we set the harvest at 100 females and 100 males per year; from 2026 onwards we set it to 50 females and 200 males per year.
<br>
<img align="middle" style="padding: 3px" width="600" src="assets/images/screencap-5.PNG">
<br>
Next, open the Scenario Properties window for the ***4x Harvest Scenario***. Review the Properties tabs for this Scenario; you will notice that the model inputs within the **Harvest** tab contains Mean havest values per year that are 4 times greater than those of the ***Baseline Scenario***.
<br>
<img align="middle" style="padding: 3px" width="600" src="assets/images/screencap-6.PNG">
<br>
Close the Scenarios you have open, then select the **File | Save** menu. You are now ready to do your first run of the model.

## **Step 4: Run the model**
Right-click on the ***Baseline Scenario*** in the **Scenario Manager** window and select **Run** from the context menu.  If prompted to save your project click **Yes**. The run should complete quickly. If the run is successful you will see a Status of **Done** in the **Run Monitor** window, at which point you can close the **Run Monitor** window; otherwise click on the **Run Log** link to see a report of any problems, make any necessary changes to your Scenario, and then re-run the Scenario.
<br>
<img align="middle" style="padding: 3px" width="600" src="assets/images/screencap-7.PNG">
<br>
Run the ***4x Harvest Scenario*** next by repeating the steps above.

## **Step 5: Analyze the results**
