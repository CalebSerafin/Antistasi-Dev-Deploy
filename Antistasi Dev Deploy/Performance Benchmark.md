# Performance Benchmarks
*CPU Bottleneck Test: Should consistently peak 100% Utilization.*<br/>
*DISCLAIMER: These were recorded by hand. UI delays will greatly affect smaller times.*<br/>

| Version  | CPU                                          | Templates | Changes | PBO   | Total Time(s) | Output MB | MB/s  |
|----------|----------------------------------------------|-----------|---------|-------|---------------|-----------|-------|
| 4.4.12.6 | CPU: Intel(R) Core(TM) i7-6700 CPU @ 3.40GHz | 12        | AltInit | False | 1             | 0.001     | 0.001 |
| 4.4.12.6 | CPU: Intel(R) Core(TM) i7-6700 CPU @ 3.40GHz | 12        | Altis   | False | 2             | 5         | 2.5   |
| 4.4.12.6 | CPU: Intel(R) Core(TM) i7-6700 CPU @ 3.40GHz | 12        | 12      | False | 10            | 61        | 6.1   |
| 4.4.12.5 | CPU: Intel(R) Core(TM) i7-6700 CPU @ 3.40GHz | 100       | 100     | False | 71            | 511       | 7.197 |
| 4.4.12.5 | CPU: Intel(R) Core(TM) i7-6700 CPU @ 3.40GHz | 100       | 100     | False | 65            | 511       | 7.862 |
| 4.4.12.5 | CPU: Intel(R) Core(TM) i7-6700 CPU @ 3.40GHz | 100       | 100     | True  | 99            | 1029      | 10.40 |