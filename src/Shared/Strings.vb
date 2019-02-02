'*********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Module Strings

    'Common column names
    Public Const DATASHEET_ITERATION_COLUMN_NAME As String = "Iteration"
    Public Const DATASHEET_TIMESTEP_COLUMN_NAME As String = "Timestep"
    Public Const DATASHEET_JULIAN_DAY_COLUMN_NAME As String = "JulianDay"
    Public Const DATASHEET_STRATUM_ID_COLUMN_NAME As String = "StratumID"
    Public Const DATASHEET_AGE_CLASS_ID_COLUMN_NAME As String = "AgeClassID"
    Public Const DATASHEET_SEX_COLUMN_NAME As String = "Sex"
    Public Const DATASHEET_MIN_AGE_COLUMN_NAME As String = "MinimumAge"
    Public Const DATASHEET_MAX_AGE_COLUMN_NAME As String = "MaximumAge"
    Public Const DATASHEET_MEAN_COLUMN_NAME As String = "Mean"
    Public Const DATASHEET_DISTRIBUTION_TYPE_COLUMN_NAME = "DistributionType"
    Public Const DATASHEET_DISTRIBUTION_SD_COLUMN_NAME = "DistributionSD"
    Public Const DATASHEET_DISTRIBUTION_MIN_COLUMN_NAME = "DistributionMin"
    Public Const DATASHEET_DISTRIBUTION_MAX_COLUMN_NAME = "DistributionMax"
    Public Const DATASHEET_HAS_CENSUS_DATA_COLUMN_NAME As String = "HasCensusData"

    'Stratum data sheet
    Public Const STRATUM_DATASHEET_NAME As String = "DGSim_Stratum"

    'Age Class data sheet
    Public Const AGE_CLASS_DATASHEET_NAME As String = "DGSim_AgeClass"

    'Run Control data sheet
    Public Const RUN_CONTROL_DATASHEET_NAME As String = "DGSim_RunControl"
    Public Const RUN_CONTROL_DATASHEET_START_JULIAN_DAY_COLUMN_NAME As String = "StartJulianDay"

    'Initial population size data sheet
    Public Const INITIAL_POPULATION_SIZE_DATASHEET_NAME As String = "DGSim_InitialPopulationSize"

    'Initial population distribution data sheet
    Public Const INITIAL_POPULATION_DISTRIBUTION_DATASHEET_NAME As String = "DGSim_InitialPopulationDistribution"
    Public Const INITIAL_POPULATION_DISTRIBUTION_DATASHEET_RELATIVE_AMOUNT_COLUMN_NAME As String = "RelativeAmount"

    'Census data sheet
    Public Const CENSUS_DATASHEET_NAME As String = "DGSim_Census"
    Public Const CENSUS_DATASHEET_MIN_POP_COLUMN_NAME As String = "MinimumPopulation"
    Public Const CENSUS_DATASHEET_MAX_POP_COLUMN_NAME As String = "MaximumPopulation"
    Public Const CENSUS_DATASHEET_MIN_M2F_RATIO_COLUMN_NAME As String = "MinimumMToFRatio"
    Public Const CENSUS_DATASHEET_MAX_M2F_RATIO_COLUMN_NAME As String = "MaximumMToFRatio"

    'Age class range data sheet
    Public Const AGE_CLASS_RANGE_DATASHEET_NAME As String = "DGSim_AgeClassRange"

    'Offspring Per Female data feed
    Public Const OFFSPRING_PER_FEMALE_OPTION_DATASHEET_NAME As String = "DGSim_OffspringPerFemaleOption"
    Public Const OFFSPRING_PER_FEMALE_VALUE_DATASHEET_NAME As String = "DGSim_OffspringPerFemaleValue"
    Public Const OFFSPRING_PER_FEMALE_VALUE_BIRTH_JDAY_COLUMN_NAME As String = "BirthJulianDay"
    Public Const OFFSPRING_PER_FEMALE_COUNT_JULIAN_DAY_COLUMN_NAME As String = "CountJulianDay"

    'Annualized mortality rate data sheet
    Public Const ANNUALIZED_MORTALITY_RATE_DATASHEET_NAME As String = "DGSim_AnnualizedMortalityRate"

    'Annual harvest data feed
    Public Const ANNUAL_HARVEST_OPTION_DATASHEET_NAME As String = "DGSim_AnnualHarvestOption"
    Public Const ANNUAL_HARVEST_VALUE_DATASHEET_NAME As String = "DGSim_AnnualHarvestValue"
    Public Const ANNUAL_HARVEST_SPECIFICATION_COLUMN_NAME As String = "Specification"
    Public Const ANNUAL_HARVEST_POP_FILTER_SEX_COLUMN_NAME As String = "PopFilterSex"
    Public Const ANNUAL_HARVEST_POP_FILTER_MIN_AGE_COLUMN_NAME As String = "PopFilterMinAge"
    Public Const ANNUAL_HARVEST_POP_FILTER_MAX_AGE_COLUMN_NAME As String = "PopFilterMaxAge"

    'Demographic Rate Shift Data Sheet
    Public Const DEMOGRAPHIC_RATE_SHIFT_DATASHEET_NAME As String = "DGSim_DemographicRateShift"
    Public Const DEMOGRAPHIC_RATE_SHIFT_FECUNDITY_COLUMN_NAME As String = "Fecundity"
    Public Const DEMOGRAPHIC_RATE_SHIFT_MORTALITY_COLUMN_NAME As String = "Mortality"

    'Output population size data sheet
    Public Const OUTPUT_POPULATION_SIZE_DATASHEET_NAME As String = "DGSim_OutputPopulationSize"
    Public Const OUTPUT_POPULATION_SIZE_POPULATION_COLUMN_NAME As String = "Population"

    'Output harvest data sheet
    Public Const OUTPUT_HARVEST_DATASHEET_NAME As String = "DGSim_OutputHarvest"
    Public Const OUTPUT_HARVEST_HARVEST_COLUMN_NAME As String = "Harvest"

    'Output recruits data sheet
    Public Const OUTPUT_RECRUITS_DATASHEET_NAME As String = "DGSim_OutputRecruits"
    Public Const DATASHEET_MOTHER_AGECLASS_ID_COLUMN_NAME As String = "MotherAgeClassID"
    Public Const DATASHEET_OFFSPRING_SEX_COLUMN_NAME As String = "OffspringSex"
    Public Const OUTPUT_RECRUITS_COLUMN_NAME As String = "Recruits"

    'Output mortality data sheet
    Public Const OUTPUT_MORTALITY_DATASHEET_NAME As String = "DGSim_OutputMortality"
    Public Const OUTPUT_MORTALITY_MORTALITY_COLUMN_NAME As String = "Mortality"

    'Output Posterior Distribution Values Data Sheet
    Public Const OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_NAME As String = "DGSim_OutputPosteriorDistributionValue"
    Public Const OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_VARIABLE_COLUMN_NAME As String = "Variable"
    Public Const OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_ISFILTERED_COLUMN_NAME As String = "IsFiltered"
    Public Const OUTPUT_POSTERIOR_DIST_VALUE_DATASHEET_VALUE_COLUMN_NAME As String = "Value"

End Module
